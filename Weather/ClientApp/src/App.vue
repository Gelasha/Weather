<template>
  <div id="app">
    <b-nav tabs align="center">
      <b-nav-item active>
        <router-link class="nav-link" to="/">Home</router-link>
      </b-nav-item>
      <b-nav-item>
        <router-link class="nav-link" to="/history">History</router-link>
      </b-nav-item>
    </b-nav>
    <main class="container mt-2">
      <router-view />
    </main>
  </div>
</template>
