import { Component, OnInit } from '@angular/core';
import { NotesService } from '../services/notes.service';  // adjust path if needed

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  viewMode: string = 'list';
  notes: any[] = [];

  constructor(private notesService: NotesService) {}

  ngOnInit(): void {
    this.notesService.getNotes().subscribe(
      (data) => {
        this.notes = data;
      },
      (error) => {
        console.error('Error fetching notes:', error);
      }
    );
  }
}
