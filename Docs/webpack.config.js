const {join} = require("path")
const MiniCssExtractPlugin = require("mini-css-extract-plugin")
const HtmlWebpackPlugin = require("html-webpack-plugin")
const CopyWebpackPlugin = require("copy-webpack-plugin")

const babelOptions = {
	presets: [
		[
			"@babel/env",
			{
				targets: {
					"browsers": "defaults"
				}
			}
		]
	]
}

module.exports = {
	target: "web",
	mode: "production",
	entry: {
		main: "./js/index.js"
	},
	output: {
		filename: "js/[name].[hash].js",
		path: join(__dirname, "build"),
		publicPath: "/"
	},
	plugins: [
		new HtmlWebpackPlugin({
			template: "webpack.html",
			filename: "../_includes/webpack.hbs",
			inject: false
		}),
		// new CopyWebpackPlugin({
		// 	patterns: [
		// 		{
		// 			from: "images/**/*",
		// 			to: "",
		// 			globOptions: {
		// 				ignore: ["**/*.xcf"]
		// 			}
		// 		}
		// 	]
		// }),
		new MiniCssExtractPlugin({
			filename: "css/[name].[hash].css",
			ignoreOrder: false
		})
	],
	module: {
		rules: [
			{
				test: /\.js$/,
				exclude: /node_modules/,
				use: [
					{
						loader: "babel-loader",
						options: babelOptions
					}
				]
			},
			{
				test: /\.(sc|sa|c)ss$/,
				use: [
					{
						loader: MiniCssExtractPlugin.loader
					},
					"css-loader",
					{
						loader: "postcss-loader",
						options: {
							postcssOptions: {
								plugins: [require("autoprefixer")]
							}
						}
					},
					{
						loader: "sass-loader",
						options: {
							implementation: require("sass")
						}
					}
				]
			}
		]
	},
	resolve: {
		extensions: [
			".js"
		],
		alias: {
			"@": __dirname
		}
	},
	devtool: "source-map"
}