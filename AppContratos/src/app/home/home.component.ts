// home.component.ts

import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('hover', [
      state('in', style({ transform: 'scale(1.1)', boxShadow: '10px 5px 15px rgba(0, 0, 0, 0.3)' })),
      state('out', style({ transform: 'scale(1)', boxShadow: 'none' })),
      transition('in <=> out', [
        animate('0.5s') // Transição mais lenta
      ])
    ])
  ]
})
export class HomeComponent implements OnInit {
  hoverState1 = 'out';
  hoverState2 = 'out';
  hoverState3 = 'out';

  constructor() { }

  ngOnInit(): void {
  }

  hover(i: number): void {
    if (i === 1) this.hoverState1 = 'in';
    if (i === 2) this.hoverState2 = 'in';
    if (i === 3) this.hoverState3 = 'in';
  }

  noHover(i: number): void {
    if (i === 1) this.hoverState1 = 'out';
    if (i === 2) this.hoverState2 = 'out';
    if (i === 3) this.hoverState3 = 'out';
  }
}
