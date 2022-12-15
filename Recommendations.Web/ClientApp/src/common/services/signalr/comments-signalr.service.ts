import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {CommentModel} from "../../models/CommentModel";

@Injectable({
  providedIn: 'root'
})
export class CommentsSignalrService {
  hubConnection!: HubConnection
  public comments: CommentModel[] = []

  constructor() {}

  public async connect(): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/comments')
      .build()
    await this.hubConnection.start();
    this.addListeners()
  }

  private addListeners() {
    this.hubConnection.on('CommentNotification',
      (comment: CommentModel) => {this.comments.push(comment)})
  }

  public async connectToGroup(reviewId: string){
    await this.hubConnection.invoke("ConnectToGroup", reviewId)
  }

  public async NotifyAboutComment(reviewId: string, commentId: string) {
    await this.hubConnection.invoke("Notify", reviewId, commentId)
  }
}
