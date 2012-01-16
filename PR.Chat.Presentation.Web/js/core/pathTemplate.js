(function($, window) {
   
function pathTemplate(pathRegex, paramMapping) {
   this.pathRegex    = pathRegex;
   this.paramMapping = paramMapping;
};
pathTemplate.prototype = {
   isMatch : function (hash) {
      return this.pathRegex.test(hash || location.hash);
   },
   getParameters : function (hash) {
      hash = hash || location.hash;

      var regexMatch = this.pathRegex.exec(hash);
      if (regexMatch) {
         var result = { };
         $(paramMapping).each(function(index, paramName) {
            result[paramName] = regexMatch[index] || null;
         });
         
         return result;
      }
      return null;
   }
};
   
window.core = window.core || { };
window.core.pathTemplate = pathTemplate;
   
})(jQuery, window);