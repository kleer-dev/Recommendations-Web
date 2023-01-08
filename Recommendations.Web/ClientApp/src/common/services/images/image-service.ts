import {Injectable} from "@angular/core";
import {firstValueFrom, Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) {

  }

  async getImages(files: File[], urls: string[]): Promise<File[]> {
    let images = await Promise.all(urls.map(url => firstValueFrom(this.http.get(url,
      {responseType: 'blob'}))));
    return images.map((image, index) => {
      return new File([<BlobPart>image], `${index}/png`,
        {type: `image/png`});
    })
  }

  async convertToBase64(image: HTMLImageElement): Promise<string> {
    let imageBlob = await firstValueFrom(this.convertToBlob(image))
    return await this.convertBlobToBase64(imageBlob)
  }

  async convertBlobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => {
        resolve(reader.result as string);
      };
      reader.onerror = reject;
      reader.readAsDataURL(blob);
    });
  }

  convertToBlob(image: HTMLImageElement): Observable<Blob> {
    return this.http.get(image.src, {responseType: 'blob'})
  }

}
