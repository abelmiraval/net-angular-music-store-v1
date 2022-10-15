import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterViewInit {
	title = 'angular-music-store';
	constructor() {
		console.log('--constructor--');

		this.title = "Hello world"
	}

	ngOnInit(): void {
		console.log('---ngOnInit ---')
	}

	ngAfterViewInit(): void {
		console.log('---ngAfterViewInit ---')
	}
}
