export class Posting {
  public id: number;
  public title: string;
  public employer: string;
  public requirements: string;
  public description: string;
  public additionalInformation?: string;
  public contactInformation: string;
  public questions: string[];
}

export class Application {
  public name: string;
  public email: string;
  public phone: string;
  public resume: string;
  public postings: Posting[];
}
