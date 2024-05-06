import { Component, OnInit } from '@angular/core';
import { AuthService, ThemesService } from '@app/services';
import Chart, { ChartConfiguration } from 'chart.js/auto';
import moment from 'moment';

@Component({
  selector: 'app-graph',
  standalone: true,
  imports: [],
  templateUrl: './graph.component.html',
  styleUrl: './graph.component.scss'
})
export class GraphComponent implements OnInit {
  chart: any;

  constructor(private themesService: ThemesService, private authService: AuthService) { }

  randomize(min = 1, max = 100) {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  pushValIntoDataAry(min = 1, max = 100) {
    var ary = [];
    for(var k = 0; k < max; k++) {
      ary.push(this.randomize(min, max));
    }
    return ary;
  }

  ngOnInit(): void {
    this.getChart();
  }

  getMonthsNames() {
    return ['January', 'Feburary', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  }

  getColorText() {
    return this.themesService.getTheme()?.includes("dark") ? 'white' : 
    this.themesService.getTheme()?.includes("glass") ? 'white' : 
    'black';
  }

  getColorGrid() {
    return this.themesService.getTheme()?.includes("dark") ? 'rgba(255,255,255,.3)' : 
    this.themesService.getTheme()?.includes("glass") ? 'rgba(255,255,255,.3)' : 
    'rgba(0,0,0,.3)';
  }

  getDataSets(lblsary: string[]) {
    return [
      {
        label: 'Luigi\'s Posts',
        data: this.pushValIntoDataAry(1, lblsary.length),
        borderColor: 'rgba(255,0,0,1)',
        backgroundColor: 'rgba(255,0,0,.5)',
        borderWidth: 2,
        borderRadius: 5,
      },
      {
        label: 'Mario\'s Posts',
        data: this.pushValIntoDataAry(1, lblsary.length),
        borderColor: 'rgba(0,255,0,1)',
        backgroundColor: 'rgba(0,255,0,.5)',
        borderWidth: 2,
        borderRadius: 5,
      },
      {
        label: 'Guest\'s Posts',
        data: this.pushValIntoDataAry(1, lblsary.length),
        borderColor: 'rgba(0,0,255,1)',
        backgroundColor: 'rgba(0,0,255,.5)',
        borderWidth: 2,
        borderRadius: 5,
      }
    ];
  }

  getOptionsGraph(colortxt: string, colorgrid: string = ""): any {
    var myuinfo = this.authService.getCurUserInfoAuth();
    var datanow = moment().format("YYYY-MM-DD HH:mm");

    return {
      responsive: true,
      maintainAspectRatio: false,
      color: colortxt,
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            color: colortxt
          },
          grid: {
            color: colorgrid ?? "rgba(255,255,255,.5)"
          }
        },
        x: {
          ticks: {
            color: colortxt
          },
          grid: {
            color: colorgrid ?? "rgba(255,255,255,.5)"
          }
        }
      },
      plugins: {
        title: {
          display: true,
          text: 'Posts',
          color: colortxt,
          position: 'top',
          align: 'center',
          padding: 10
        },
        subtitle: {
          display: true,
          text: `Fetched data chart by ${myuinfo.username} on ${datanow}`,
          color: colortxt,
          position: 'bottom',
          align: 'center',
          padding: 10
        }
      }
    };
  }

  getDataGraph(lblsary: string[]) {
    return {
      labels: lblsary, 
      datasets: this.getDataSets(lblsary)
    };
  }

  loadConfigGraph(): ChartConfiguration {
    var lblsary = this.getMonthsNames();
    var colortxt = this.getColorText();
    var colorgrid = this.getColorGrid();

    return {
      type: "bar",
      data: this.getDataGraph(lblsary),
      options: this.getOptionsGraph(colortxt, colorgrid)
    };
  }

  getChart() {
    this.chart = new Chart("mychart", this.loadConfigGraph());
  }
}
