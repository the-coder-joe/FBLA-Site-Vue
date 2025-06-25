export class Posting {
  public id: number;
  public title: string;
  public employer: string;
  public requirements: string;
  public description: string;
  public additionalInformation?: string;
  public contactInformation: string;
  public questions: string[];
  public submittedById: number;
}

export class Application {
  public id: number;
  forPostingId: number;
  public name: string;
  public contactInfo: string;
  public answers: string[];
}

export class PostingRequest extends Posting {
  public submittedById: number;
}
