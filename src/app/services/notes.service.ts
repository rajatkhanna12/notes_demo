import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class NotesService {
  //private apiUrl = 'https://localhost:7082/api/Notes';
  private apiUrl = `${environment.apiUrl}/Notes`; // Use the environment variable

  constructor(private http: HttpClient) {}

  getNotes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
