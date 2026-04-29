export interface Token {
  token_type: string;
  access_token: string;
  refresh_token: string;
  expire_time: number;
  scope: string;
}