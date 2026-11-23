/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,jsx,ts,tsx}'],
  theme: {
    extend: {
      colors: {
        beige: {
          50: '#fdfaf3',
          100: '#f7f0e1',
          200: '#efe3cb',
          300: '#e2d2b3',
          400: '#cdb690',
          500: '#b3946a',
          600: '#8f7248',
          700: '#6f5836',
          800: '#4e3d26',
          900: '#332818',
        },
      },
      fontFamily: {
        sans: ['Inter', 'Segoe UI', 'system-ui', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
