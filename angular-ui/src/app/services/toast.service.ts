import {inject, Injectable} from '@angular/core';
import {MessageService} from 'primeng/api';

/**
 * Code from <a href="https://primeng.org/toast">Primeng Toast</a>.
 */
@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private messageService = inject(MessageService)

  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: message });
  }

  showInfo(message: string) {
    this.messageService.add({ severity: 'info', summary: 'Info', detail: message });
  }

  showWarn(message: string) {
    this.messageService.add({ severity: 'warn', summary: 'Warn', detail: message });
  }

  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message });
  }
}
