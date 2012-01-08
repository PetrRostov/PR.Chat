window.route = function route(pathTemplate, viewName, viewConstructor) {
   this.pathTemplate    = pathTemplate;
   this.viewName        = viewName;
   this.viewConstructor = viewConstructor;
};