const FORECAST = `https://localhost:44306/weatherforecast/Forecast`;
const SEARCH = `https://localhost:44306/weatherforecast/Weather/`;
const SEARCH_HISTORY = `https://localhost:44306/weatherforecast/GetSearchHistory`;

function getWeatherForCities() {
  return fetch(`${FORECAST}`).then(response => response.json());
}

function search(param) {
  return fetch(`${SEARCH}${param}`).then(response => response.json());
}

function getSearchHistory() {
  return fetch(`${SEARCH_HISTORY}`).then(response => response.json());
}

export default {
  getWeatherForCities,
  search,
  getSearchHistory
};
