const visit = require("unist-util-visit");
const { getCacheKey } = require("../shared");

module.exports = async ({ markdownAST, markdownNode, cache, getNode }) => {
  // if there's no slug, it is not a markdown file we're interested in
  if (!markdownNode.fields || !markdownNode.fields.slug) {
    return markdownAST;
  }

  const headingAnchors = [];
  const links = [];

  // collect headings in the current file
  visit(markdownAST, "heading", (node) => {
    // this id is generated by gatsby-remark-autolink-headers
    headingAnchors.push(node.data.id);
  });

  // collect all links from the current file
  visit(markdownAST, "link", (node, _, parent) => {
    // ignore the little anchor next to a heading
    if (parent.type === "heading") {
      return;
    }

    const isHttpLink = /^https?\:\/\//.test(node.url);

    // if this is a link that does not point to one of our documents
    if (isHttpLink) {
      return;
    }

    links.push({
      position: node.position,
      url: node.url,
      frontmatter: markdownNode.frontmatter,
    });
  });

  // collect all links from definitions (non-inline links)
  visit(markdownAST, "definition", (node, _, parent) => {
    const isHttpLink = /^https?\:\/\//.test(node.url);

    // if this is a link that does not point to one of our documents
    if (isHttpLink) {
      return;
    }

    links.push({
      position: node.position,
      url: node.url,
      frontmatter: markdownNode.frontmatter,
    });
  });

  // get the file this markdown belongs to
  const parent = await getNode(markdownNode.parent);
  const cacheKey = getCacheKey(parent);

  cache.set(cacheKey, {
    slug: markdownNode.fields.slug,
    links,
    headingAnchors: headingAnchors,
    absolutePath: parent.absolutePath,
  });

  return markdownAST;
};
