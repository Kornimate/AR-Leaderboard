const ENVIRONMENTS = Object.freeze({
    DEBUG: 0,
    PRODUCTION: 1
})

const APP_ENV = ENVIRONMENTS.PRODUCTION;

const CONFIGURATION = Object.freeze({
    BASE_URL : APP_ENV === ENVIRONMENTS.DEBUG ? "http://localhost:5271" : "https://matekorni-001-site1.jtempurl.com", //TODO finish it when web api hosted
    API_ENDPOINT: "/api/leaderboard",
    SIGNALR_ENDPOINT : "/signalr/updates"
})

export default CONFIGURATION;