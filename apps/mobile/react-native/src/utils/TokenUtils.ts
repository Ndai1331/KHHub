export function isTokenValid(token) {
  if (!token || typeof token !== 'object' || !token.expire_time) return false;

  const now = new Date().valueOf();

  return now < token.expire_time;
}

export function isTokenExpiringSoon(token, bufferMs = 5 * 60 * 1000) {
  if (!isTokenValid(token)) {
    return false;
  }

  const now = new Date().valueOf();
  const timeUntilExpiry = token.expire_time - now;

  return timeUntilExpiry < bufferMs;
}
