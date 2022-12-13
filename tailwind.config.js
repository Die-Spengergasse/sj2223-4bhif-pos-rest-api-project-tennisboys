/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/Spg.TennisBooking.MvcFrontEnd/**/*.cshtml"],
  theme: {
  extend: {
    colors: {
      "custom": {
        100: "#E8F4FF",
        200: "#6A8EAE",
        300: "#5B83AB",
        400: "#545364",
      }
    }
  },
  },
  plugins: [],
}
