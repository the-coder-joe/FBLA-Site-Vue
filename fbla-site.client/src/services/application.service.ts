import { Posting } from '@/models/application.models.js';
import { BASE_URL} from '../models/constants';


export default class ApplicationService {
  constructor() {

  }

  public async addPosting (posting: Posting): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/AddPosting`);

    const request = new Request(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(posting)
    });

    const response = await fetch(url, request);

    return response.json();
  }



}
