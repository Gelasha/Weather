<template>
  <div>
    <div class="input-group md-form form-sm form-2 pl-0">
      <input
        class="form-control my-0 py-1 amber-border"
        type="search"
        placeholder="Search"
        aria-label="Search"
        v-model="term"
      />
      <b-button variant="outline-primary" @click="search">Search</b-button>
    </div>
    <div>
      <table class="table">
        <thead>
          <th scope="col"></th>
          <th
            scope="col"
            v-show="showListOfCity"
            v-for="i in forecast.date"
            v-bind:key="i.id"
          >{{ i }}</th>
        </thead>
        <tbody>
          <tr
            v-show="showListOfCity"
            v-for="item in forecast.basicFieldsByCity"
            v-bind:key="item.id"
          >
            <td>{{ item.city }}</td>
            <td v-for="i in item.fields" v-bind:key="i.id">
              <div>Spead: {{ i.averageSpead }}</div>
              <div>Temprecher: {{ i.averageTemp }}</div>
            </td>
          </tr>
          <tr v-show="showCity">
            <td>{{ weather.city }}</td>
            <td>{{ weather.humadity }}</td>
            <td>{{ weather.temp }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import API from "@/lib/API";
export default {
  data() {
    return {
      forecast: [],
      term: "",
      forecastByCity: null,
      showListOfCity: true,
      weather: "",
      showCity: false
    };
  },
  mounted: async function() {
    this.loadWeather();
  },
  methods: {
    loadWeather() {
      API.getWeatherForCities().then(result => {
        this.forecast = result;
      });
    },
    search() {
      API.search(this.term).then(result => {
        this.weather = result;
        this.showListOfCity = false;
        this.showCity = true;
      });
    }
  }
};
</script>
