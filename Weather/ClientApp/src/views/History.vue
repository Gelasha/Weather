<template>
  <div>
    <b-table striped hover :items="forecast"></b-table>
  </div>
</template>

<script>
import API from "@/lib/API";
export default {
  data() {
    return {
      forecast: []
    };
  },
  mounted: async function() {
    this.loadWeather();
  },
  methods: {
    loadWeather() {
      API.getSearchHistory().then(result => {
        this.forecast = result;
      });
    }
  }
};
</script>