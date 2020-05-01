hljs.initHighlightingOnLoad();

window.replace = (id, content) => {
    var result = document.querySelector('#' + id);
    console.log("Found" + result);
    if (result != null) {
        result.textContent = content;
    }
    //result.content = content;
};

window.highlight = () => {
    //hljs.initHighlightingOnLoad();
    document.querySelectorAll('pre code').forEach((block) => {
        hljs.highlightBlock(block);
    });
    console.log("highlighed!");
};