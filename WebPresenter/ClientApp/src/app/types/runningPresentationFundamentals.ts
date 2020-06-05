import {PresentationFundamentals} from "./presentationFundamentals";
import {HttpClient} from "@angular/common/http";

export class RunningPresentationFundamentals extends PresentationFundamentals {
  public id: string;

  public end(http: HttpClient, callback: (successful: boolean, error: Error) => void) {
    http.delete(`/data/presentations/${this.id}`)
      .subscribe(
        () => callback(true, null),
        error => callback(false, error)
      );
  }
}
