/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/Spg.TennisBooking.MvcFrontEnd/**/*.cshtml"],
  theme: {
  extend: {
    colors: {
      "custom": {
        100: "#e6f1ff",
        200: "#b3d4ff",
        300: "#80b7ff",
        400: "#545364",
      }
    }
  },
  },
  plugins: [],
}
