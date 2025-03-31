import { Posting } from '@/models/application.models.js'; // Import the Posting type for type-checking
import { BASE_URL } from '../models/constants'; // Import the base URL for API requests

export default class ApplicationService {
  public async addPosting (posting: Posting): Promise<any> {
    const url = new URL(`${BASE_URL}/api/JobApplication/AddPosting`);

    const request = new Request(url, {
      method: 'POST', // Use the POST method to submit data
      headers: {
        'Content-Type': 'application/json', // Indicate that the body is JSON
      },
      body: JSON.stringify(posting) // Convert the posting object to a JSON string for the request body
    });

    const response = await fetch(url, request); // Send the request to the server

    return response.json(); // Return the response as a JSON object
  }

  // Function to fetch all job postings
  public async getPostings(): Promise<Posting[]> {
    const url = new URL(`${BASE_URL}/api/JobApplication/GetPostings`); // Construct the URL for fetching postings

    const request = new Request(url, {
      method: 'GET', // Use the GET method to retrieve data
      headers: {
        'Content-Type': 'application/json', // Indicate that the expected response is JSON
      }
    });

    const response = await fetch(url, request); // Send the request to the server

    return response.json(); // Return the response as a JSON array of postings
  }

  // Function to fetch a specific posting by its ID
  public async getPosting(postingId: number): Promise<Posting> {
    const postings = await this.getPostings(); // Get all postings first

    // Find the posting that matches the provided postingId
    const posting = postings.find((posting) => posting.id === postingId);
    if (!posting) {
      throw new Error(`Posting with id ${postingId} not found`); // If no posting is found, throw an error
    }

    return posting; // Return the found posting
  }
}
