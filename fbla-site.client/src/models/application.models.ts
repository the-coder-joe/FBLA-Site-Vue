export class Posting {
  public title: string;
  public description: string;
  public questions: string[];
}

export class Application {
  public name: string;
  public email: string;
  public phone: string;
  public resume: string;
  public postings: Posting[];
}
