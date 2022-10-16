//sources: 
// https://stackoverflow.com/a/42118921
// https://webpack.js.org/guides/asset-modules/
// https://webpack.js.org/guides/dependency-management/#requirecontext
function importAll(r) {
    return r.keys().map(r);
}

export { importAll }