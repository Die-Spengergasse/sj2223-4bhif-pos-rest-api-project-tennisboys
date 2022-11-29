/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/Spg.TennisBooking.MvcFrontEnd/**/*.cshtml"],
  theme: {
  extend: {
    colors: {
      "custom-blue": {
        100: "#e6f1ff",
        200: "#b3d4ff",
        300: "#80b7ff",
      }
    }
  },
  },
  plugins: [],
}
