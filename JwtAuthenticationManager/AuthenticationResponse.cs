﻿namespace JwtAuthenticationManager
{
    public class AuthenticationResponse
    {
        public string EmailAddress { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
