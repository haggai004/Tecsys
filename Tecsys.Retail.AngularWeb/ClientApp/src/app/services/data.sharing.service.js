import { Injectable } from '@angular/core'
import { BehaviorSubject } from 'rxjs'
@Injectable()
export class DataSharingService {
  initialVal: String = '';
  public productSearchText: BehaviorSubject<String> = new BehaviorSubject<String>(this.initialVal);
}
# sourceMappingURL=data.sharing.service.js.map
