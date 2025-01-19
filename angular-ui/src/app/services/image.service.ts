import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ImageIdDto} from '../dto/image-dto';

@Injectable({ providedIn: 'root' })
export class ImageService {

  constructor(private http: HttpClient) {}

  getImage(imageId: string): Observable<Blob> {
    return this.http.get(`/image/${imageId}`, { responseType: 'blob' });
  }

  uploadImage(file: Blob, queryParams: { [key: string]: string | undefined }): Observable<ImageIdDto> {
    const formData = new FormData();
    formData.append('file', file);

    let params = new HttpParams();
    Object.entries(queryParams).forEach(([key, value]) => {
      if (value) params = params.set(key, value);
    });

    return this.http.post<ImageIdDto>('/image', formData, { params });
  }

  deleteImage(imageId: string): Observable<void> {
    return this.http.delete<void>(`/image/${imageId}`);
  }
}
