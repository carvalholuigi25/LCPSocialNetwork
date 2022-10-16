const path = require("path");
const common = require("./webpack.common");
const {merge} = require("webpack-merge");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const TerserPlugin = require("terser-webpack-plugin");

const functions = require('./functions');
const getbase = functions.getBase();
const pagesary = functions.getPages(true);
const minifyopts = functions.setMinifyForWPConfig();

module.exports = merge(common, {
  mode: "production",
  devServer: {
    port: 3000,
    hot: true
  },
  output: {
    filename: "[name].[contenthash].bundle.js",
    path: path.resolve(__dirname, "dist"),
    clean: true,
  },
  performance: {
    hints: false,
    maxEntrypointSize: 30000000,
    maxAssetSize: 30000000
  },
  optimization: {
    minimizer: [
      new CssMinimizerPlugin(),
      new TerserPlugin()
    ].concat(pagesary)
  },
  plugins: [
    new MiniCssExtractPlugin({ filename: "[name].[contenthash].css" })
  ],
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          MiniCssExtractPlugin.loader, //3. Extract css into files
          "css-loader", //2. Turns css into commonjs
          "sass-loader" //1. Turns sass into css
        ]
      }
    ]
  }
});