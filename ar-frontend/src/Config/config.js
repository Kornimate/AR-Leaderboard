const ENVIRONMENTS = Object.freeze({
    DEBUG: 0,
    PRODUCTION: 1
})

const APP_ENV = ENVIRONMENTS.DEBUG;

const CONFIGURATION = Object.freeze({
    BASE_URL : APP_ENV === ENVIRONMENTS.DEBUG ? "http://localhost:5271" : "", //TODO finish it when web api hosted
    API_ENDPOINT: "/api/leaderboard",
    SIGNALR_ENDPOINT : "/signalr/updates"
})

export default CONFIGURATION;