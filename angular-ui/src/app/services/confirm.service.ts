import {inject, Injectable} from '@angular/core';
import {ConfirmationService} from 'primeng/api';
import {ConfirmType} from '../enum/confirm-type.enum';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  private confirmationService = inject(ConfirmationService)

  confirm(options: {
    message: string
    type: ConfirmType
    header?: string
    icon?: string
    accept: () => void
    reject?: () => void
  }) {
    this.confirmationService.confirm({
      message: options.message,
      header: options.header || 'Confirmation',
      icon: options.icon || 'pi pi-exclamation-triangle',
      accept: options.accept,
      reject: options.reject,
    })
  }
}
