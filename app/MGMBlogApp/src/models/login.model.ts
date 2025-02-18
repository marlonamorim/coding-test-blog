export class Login {
  constructor(
    public authenticated: Boolean,
    public createAt: String,
    public expiration: String,
    public accessToken: String
  ) { }
}
