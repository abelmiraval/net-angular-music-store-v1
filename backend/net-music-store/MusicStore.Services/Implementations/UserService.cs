using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicStore.DataAccess;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Entities.Configurations;
using MusicStore.Services.Interfaces;

namespace MusicStore.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<MusicStoreUserIdentity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<UserService> _logger;

    public UserService(UserManager<MusicStoreUserIdentity> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<AppSettings> options,
        ILogger<UserService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _options = options;
        _logger = logger;
    }

    public async Task<BaseResponseGeneric<string>> RegisterAsync(DtoRegisterUser request)
    {
        var response = new BaseResponseGeneric<string>();

        try
        {
            var result = await _userManager.CreateAsync(new MusicStoreUserIdentity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Age = request.Age,
                TypeDocument = (TypeDocument)request.TypeDocument,
                DocumentNumber = request.DocumentNumber,
                UserName = request.Email
            }, request.Password);

            if (!result.Succeeded)
            {
                response.ListErrors = result.Errors
                    .Select(p => p.Description)
                    .ToList();
                response.Success = false;
                return response;
            }

            var userIdentity = await _userManager.FindByEmailAsync(request.Email);

            if (userIdentity != null)
            {
                if (!await _roleManager.RoleExistsAsync(Constants.RoleAdministrator))
                    await _roleManager.CreateAsync(new IdentityRole(Constants.RoleAdministrator));

                if (!await _roleManager.RoleExistsAsync(Constants.RoleCustomer))
                    await _roleManager.CreateAsync(new IdentityRole(Constants.RoleCustomer));

                if (await _userManager.Users.CountAsync() == 1)
                {
                    await _userManager.AddToRoleAsync(userIdentity, Constants.RoleAdministrator);
                }
                else
                {
                    await _userManager.AddToRoleAsync(userIdentity, Constants.RoleCustomer);
                }

                response.Result = userIdentity.Id;
            }

            response.Success = result.Succeeded;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ListErrors.Add(ex.Message);
            _logger.LogCritical(ex.Message);
        }

        return response;
    }

    public async Task<DtoLoginResponse> LoginAsync(DtoLogin request)
    {
        var response = new DtoLoginResponse();

        try
        {
            var identity = await _userManager.FindByEmailAsync(request.Email);

            if (identity == null)
            {
                throw new ApplicationException(Constants.UserDoesntExists);
            }

            if (!await _userManager.CheckPasswordAsync(identity, request.Password))
            {
                throw new ApplicationException(Constants.InvalidPassword);
            }

            var expiredDate = DateTime.Now.AddHours(1);

            response.FullName = $"{identity.FirstName} {identity.LastName}";

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, response.FullName),
                new(ClaimTypes.Email, identity.Email),
                new(ClaimTypes.Sid, identity.Id)
            };

            var roles = await _userManager.GetRolesAsync(identity);

            response.Roles = new List<string>();
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
                response.Roles.Add(role);
            }

            // Creamos el token

            var llavesimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SigningKey));

            var credentials = new SigningCredentials(llavesimetrica, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(
                issuer: _options.Value.Jwt.Issuer,
                audience: _options.Value.Jwt.Audience,
                claims: authClaims,
                notBefore: DateTime.Now,
                expires: expiredDate);

            var token = new JwtSecurityToken(header, payload);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ListErrors.Add(ex.Message);
            _logger.LogCritical(ex, "{message}", ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<string>> SendTokenToResetPasswordAsync(DtoResetPassword request)
    {
        var response = new BaseResponseGeneric<string>();

        try
        {
            var userIdentity = await _userManager.FindByEmailAsync(request.Email);

            if (userIdentity == null)
            {
                response.Success = false;
                response.ListErrors.Add($"El correo {request.Email} no existe");
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);

            // AQUI PONEMOS EL CODIGO QUE ENVIA EL CORREO.

            response.Result = token;
            response.Success = true;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ListErrors.Add(ex.Message);
            _logger.LogCritical(ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<string>> ResetPassword(DtoConfirmReset request)
    {
        var response = new BaseResponseGeneric<string>();

        try
        {
            var userIdentity = await _userManager.FindByEmailAsync(request.Email);

            if (userIdentity == null)
            {
                response.Success = false;
                response.ListErrors.Add($"El correo {request.Email} no existe");
                return response;
            }

            var identity = await _userManager.ResetPasswordAsync(userIdentity, request.Token, request.Password);

            // AQUI VA EL CORREO CON EL MENSAJE DEL CAMBIO EXITOSO DE LA CONTRASEÑA

            response.Result = userIdentity.Email;
            response.Success = identity.Succeeded;
            response.ListErrors = identity.Errors
                .Select(p => p.Description)
                .ToList();

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ListErrors.Add(ex.Message);
            _logger.LogCritical(ex.Message);
        }

        return response;
    }
}