import {Component, OnInit} from "@angular/core";

@Component({
  selector: 'app-review',
  templateUrl: 'review.component.html',
  styleUrls: ['review.component.css']
})
export class ReviewComponent implements OnInit{

  rate: number = 0;
  like: boolean = false;

  constructor () {

  }

  ngOnInit(): void {

  }

  onRateChange(e: any){

  }

  onLike(){
    let likeBtn = document.getElementById('likeBtn')
    if (!this.like){
      this.like = true
      likeBtn!.classList.remove("btn-dark")
      likeBtn!.classList.add("btn-success")
    }
    else{
      this.like = false
      likeBtn!.classList.remove("btn-dark")
      likeBtn!.classList.add("btn-dark")
    }
  }

  checkLikeStatus(){
    let likeBtn = document.getElementById('likeBtn')
    if (this.like){
      likeBtn!.classList.remove("btn-dark")
      likeBtn!.classList.add("btn-success")
    }
    else{
      likeBtn!.classList.remove("btn-dark")
      likeBtn!.classList.add("btn-dark")
    }
  }
}
