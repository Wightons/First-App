import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { LogDto } from 'src/Dtos/LogDto';
import { LogsService } from 'src/services/logs.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() isOpen: boolean = false;

  logs$: Observable<LogDto[]> | null = null;

  constructor(private logsService: LogsService, private sanitizer: DomSanitizer){

  }

  sanitize(html: string) {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

  toggleSidebar() {
    this.isOpen = !this.isOpen;
    this.logs$ = this.logsService.getLogs();
  }

  ngOnInit() {
    this.logs$ = this.logsService.getLogs();
  }
}
