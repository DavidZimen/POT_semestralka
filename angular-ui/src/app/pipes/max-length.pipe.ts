import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'maxLength'
})
export class MaxLengthPipe implements PipeTransform {

  transform(value: string | undefined | null, limit: number): string{
    if (!value) return ''
    if (value.length > limit) return value.substring(0, limit) + '...';
    else return value;
  }

}
