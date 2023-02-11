const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:58698';

const PROXY_CONFIG = [
  {
    context: [
      "/api/user",
      "/api/home",
      "/api/reviews",
      "/api/tags",
      "/api/likes",
      "/api/ratings",
      "/api/comments",
      "/api/categories",
      "/api/search",
      "/api/products",
      "/signin-google",
      "/signin-spotify",
      "/comments"
   ],
    target: target,
    secure: false,
    ws: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
