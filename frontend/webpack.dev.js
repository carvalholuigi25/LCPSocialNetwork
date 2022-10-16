const path = require("path");
const common = require("./webpack.common");
const {merge} = require("webpack-merge");
const functions = require("./functions");
const pagesary = functions.getPages(false);

module.exports = merge(common, {
  mode: "development",
  output: {
    filename: "[name].bundle.js",
    path: path.resolve(__dirname, "dist")
  },
  devServer: {
    port: 3000,
    hot: true
  },
  plugins: [].concat(pagesary),
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          "style-loader", //3. Inject styles into DOM
          "css-loader", //2. Turns css into commonjs
          "sass-loader" //1. Turns sass into css
        ]
      }
    ]
  }
});