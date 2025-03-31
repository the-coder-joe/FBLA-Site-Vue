import { BASE_URL } from "@/models/constants";
import { pbkdf2Sync, randomBytes } from 'crypto';

class AuthenticationService {
  private baseUrl: string;

  public async login(username: string, password: string): Promise<any> {

  }

  public async register(username: string, password: string, email: string): Promise<any> {
  }

  logout(): void {
    // Clear any stored authentication tokens or session data
    console.log('User logged out');
  }

  private hashPassword(password: string): { salt: string; hashedPassword: string } {
    const salt = randomBytes(16).toString('hex'); // Generate a random salt
    const iterations = 100000;
    const keylen = 64; // Length of derived key in bytes
    const digest = 'sha512'; // Hash algorithm

    const hashedPassword = pbkdf2Sync(password, salt, iterations, keylen, digest).toString('hex');

    return { salt, hashedPassword };
  }

  private async verifyPassword(hash: string): Promise<boolean> {
    const url= new URL(`${BASE_URL}/api/Authentication/VerifyPassword`);
    return false;
  }
}

export const authenticationService = new AuthenticationService();
