import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FilterOperatorEnum, QueryParams, SortEnum } from '@app/models';
import { SharedModule } from '@app/modules';
import { PostsService } from '@app/services';

@Component({
  selector: 'app-search-dialog',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponentDialog implements OnInit {
  frmSearch: FormGroup = new FormGroup({
    search: new FormControl('', Validators.required)
  });

  searchData: any;

  constructor(private postService: PostsService) {}

  ngOnInit(): void {
    this.DoSearch();
  }

  get f() { return this.frmSearch.controls; }
  
  DoSearch(){
    let myparams: QueryParams = {
      page: 1,
      pageSize: 10,
      sortOrder: "ASC",
      sortBy: "Title",
      search: this.f['search'].value ?? "",
      operator: "Contains"
    };

    this.postService.searchPosts(myparams).subscribe({
      next: (r: any) => {
      console.log(r);
       this.searchData = r.data;
      },
      error: (err) => console.log(err)
    });
  }
}
