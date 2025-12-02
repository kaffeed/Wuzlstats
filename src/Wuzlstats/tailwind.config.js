/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Views/**/*.cshtml',
    './wwwroot/js/**/*.js'
  ],
  theme: {
    extend: {
      colors: {
        'team-blue': '#ddf',
        'team-blue-bg': '#ddf',
        'team-red': '#fdd',
        'team-red-bg': '#fdd',
      },
      keyframes: {
        shimmer: {
          '0%': { backgroundPosition: '200% 0' },
          '100%': { backgroundPosition: '-200% 0' },
        }
      },
      animation: {
        shimmer: 'shimmer 2s infinite linear',
      }
    },
  },
  plugins: [],
}
