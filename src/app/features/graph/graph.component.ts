import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import Chart, { ChartConfiguration } from 'chart.js/auto';

@Component({
  selector: 'app-graph',
  standalone: true,
  imports: [],
  templateUrl: './graph.component.html',
  styleUrl: './graph.component.scss'
})
export class GraphComponent implements OnInit {
  chart: any;

  constructor() { }

  randomize(min = 1, max = 100) {
    var ary = [];
    for(var k = 0; k < 12; k++) {
      ary.push(Math.floor(Math.random() * (max - min + 1) + min));
    }
    return ary;
  }

  ngOnInit(): void {
    this.getChart();
  }

  loadConfigGraph(): ChartConfiguration {
    return {
      type: "bar",
      data: {
         labels: ['January', 'Feburary', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'], 
	       datasets: [
          {
            label: 'Luigi\'s Posts',
            data: this.randomize(1, 12),
            borderColor: 'rgba(255,0,0,1)',
            backgroundColor: 'rgba(255,0,0,.5)',
            borderWidth: 2,
            borderRadius: 5,
          },
          {
            label: 'Mario\'s Posts',
            data: this.randomize(1, 12),
            borderColor: 'rgba(0,255,0,1)',
            backgroundColor: 'rgba(0,255,0,.5)',
            borderWidth: 2,
            borderRadius: 5,
          },
          {
            label: 'Guest\'s Posts',
            data: this.randomize(1, 12),
            borderColor: 'rgba(0,0,255,1)',
            backgroundColor: 'rgba(0,0,255,.5)',
            borderWidth: 2,
            borderRadius: 5,
          }
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    };
  }

  getChart() {
    this.chart = new Chart("mychart", this.loadConfigGraph());
  }
}
