import { Application, Posting, PostingRequest } from '@/models/application.models.js';
import { BASE_URL } from '../models/constants';

export default class ApplicationService {
  public async addPosting(posting: Posting, userId: number): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/AddPosting/${userId}`);

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


  public async getPostings(): Promise<Posting[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetPostings`);

    const request = new Request(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    const response = await fetch(url, request);

    return response.json();
  }


  public async getPosting(postingId: number): Promise<Posting> {
    const postings = await this.getPostings();


    const posting = postings.find((posting) => posting.id === postingId);
    if (!posting) {
      throw new Error(`Posting with id ${postingId} not found`);
    }

    return posting;
  }

  public async getPostingsQueue(): Promise<Posting[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetPostingQueue`);
    const request = new Request(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });
    const response = await fetch(url, request);
    return response.json();
  }

  public async approvalPosting(postingId: number, approve: boolean): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/PostingApproval`);

    const request = new Request(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        id: postingId,
        isApproved: approve
      })
    });

    const response = await fetch(url, request);

    return await response.json();
  }

  public async getJobApplications(): Promise<any[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetJobApplications`);

    const request = new Request(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    const response = await fetch(url, request);

    return response.json();
  }

  public async submitJobApplication(postingId: number, application: Application): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/SubmitJobApplication`);

    const request = new Request(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        forPostingId: postingId,
        ...application
      })
    });

    const response = await fetch(url, request);

    return response.json();
  }

  public async getJobApplicationsByEmployerId(employerId: number): Promise<any[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetJobApplicationsByEmployerId/${employerId}`);

    const request = new Request(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    const response = await fetch(url, request);

    return response.json();
  }

  public async getPostingsByEmployerId(employerId: number): Promise<Posting[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetPostingsByEmployerId/${employerId}`);

    const request = new Request(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    const response = await fetch(url, request);

    return response.json();
  }

  public async submitApplication(Application: Application): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/SubmitJobApplication`);

    const request = new Request(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(Application)
    });

    const response = await fetch(url, request);

    return response.json();
  }

  public async fetchPostingStats(): Promise<{ total: number; approved: number; pending: number }> {
    const [allPostings, queuePostings] = await Promise.all([
      this.getPostings(),
      this.getPostingsQueue()
    ]);

    const total = allPostings.length + queuePostings.length;
    const approved = allPostings.length;
    const pending = queuePostings.length;

  return { total, approved, pending };
}

public async deletePosting(postingId: number): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/DeleteJobPosting/${postingId}`);

    const request = new Request(url, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      }
    });

    const response = await fetch(url, request);

    return response.json();
  }

}
