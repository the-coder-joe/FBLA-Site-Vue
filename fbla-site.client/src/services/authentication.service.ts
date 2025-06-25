import { BASE_URL } from "@/models/constants";
import * as crypto from 'crypto';
import { useAuthStore } from "@/stores/authentication.store";

export class AuthenticationService {
  private baseUrl: string;
  private authStore = useAuthStore();

  public async login(username: string, password: string): Promise<any> {
    const hashedPassword = this.hashPassword(password);
    const resopnse = await this.verifyPassword(username, hashedPassword);
    const success = resopnse.success;
    const role = resopnse.role;
    const id = resopnse.id;

    if (!success)
      return false;

    this.authStore.authenticated = true;
    this.authStore.role = role;
    return true;
  }

  public async register(username: string, password: string, role: string): Promise<any> {
    const hashedPassword = this.hashPassword(password);

    const body = {
      Email: username,
      PartiallyHashedPassword: hashedPassword,
      Role: role
    }

    const request = new Request(`${BASE_URL}/api/Users/AddUser`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(body)
    });

    const response = await fetch(request);
    const r = await response.json();
    return { success: r.success, role: r.role };
  }

  logout(): void {
    // Clear any stored authentication tokens or session data
    console.log('User logged out');
  }

  private hashPassword(password: string): string {
    const iterations = 100000;
    const keylen = 64; // Length of derived key in bytes
    const digest = 'sha512'; // Hash algorithm


    return password;
  }

  private async verifyPassword(email: string, hash: string): Promise<{ success: boolean, role: string, id: number }> {
    const url = new URL(`${BASE_URL}/api/Users/Authenticate`);
    const response = await fetch(url.toString(), {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        username: email,
        partiallyHashedPassword: hash,
      }),
    });

    const r = await response.json();
    return { success: r.success, role: r.role, id: r.id };
  }
}
