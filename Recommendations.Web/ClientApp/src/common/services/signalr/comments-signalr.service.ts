import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {CommentModel} from "../../models/CommentModel";

@Injectable({
  providedIn: 'root'
})
export class CommentsSignalrService {
  hubConnection!: HubConnection
  public comments: CommentModel[] = []

  constructor() {
  }

  public async connect(): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/comments')
      .build()
    await this.hubConnection.start();
    this.hubConnection.serverTimeoutInMilliseconds = 600000
    this.addListeners()
  }

  addListeners() {
    this.hubConnection.on('CommentNotification',
      (comment: CommentModel) => {
        this.comments.push(comment)
      })
  }

  async connectToGroup(reviewId: string) {
    await this.hubConnection.invoke("ConnectToGroup", reviewId)
  }

  async NotifyAboutComment(reviewId: string, commentId: string) {
    await this.hubConnection.invoke("Notify", reviewId, commentId)
  }

  async closeConnection() {
    await this.hubConnection.stop().finally(() => {});
  }
}
