const syntaxHighlight = require("@11ty/eleventy-plugin-syntaxhighlight");
const markdownIt = require('markdown-it');
const markdownItAnchor = require('markdown-it-anchor');

module.exports = function (eleventyConfig) {
	eleventyConfig.setLibrary('md', markdownIt().use(markdownItAnchor));
	eleventyConfig.addPlugin(syntaxHighlight, {
		templateFormats: ["md"]
	});
	eleventyConfig.setBrowserSyncConfig({
		server: {
			baseDir: "build",
			index: "index.html"
		}
	});

	eleventyConfig.addHandlebarsHelper("eq", function (a, b) {
		return a === b;
	});
	eleventyConfig.addHandlebarsHelper("neq", function (a, b) {
		return a !== b;
	});
	eleventyConfig.addHandlebarsHelper("mainMenuActiveClass", function (linkHref, pageUrl) {
		if (linkHref === "/" && pageUrl === "/"
			|| linkHref !== "/" && pageUrl.startsWith(linkHref)) {
			return "navbar__item--active";
		}
		return "";
	});
	eleventyConfig.addHandlebarsHelper("encodeURIComponent", function (baseUri, appendedUri) {
		return encodeURIComponent(`${baseUri}${appendedUri}`);
	});
	eleventyConfig.addHandlebarsHelper("courseIsInCategory", function (courses, category, url) {
		return findCourseIndex(courses, category, url) > -1;
	});
	eleventyConfig.addHandlebarsHelper("findPreviousCourse", function (courses, category, url) {
		const i = findCourseIndex(courses, category, url);
		return courses[category][i - 1];
	});
	eleventyConfig.addHandlebarsHelper("findNextCourse", function (courses, category, url) {
		const i = findCourseIndex(courses, category, url);
		return courses[category][i + 1];
	});
};

function findCourseIndex(courses, category, url) {
	return courses[category].findIndex(c => c.url === url);
}