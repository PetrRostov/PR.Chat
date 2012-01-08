(function ($, window) {

   window.templates = window.templates || { };
   templates.inner = '<%= this.a  %>';
   
   $().ready(function () {

      var mainTemplate = 
      '<% for(var i = 0; i < 6; ++i) {%>' + 
      '<%= $.jqote(templates.inner, this) %>' + 
      '<%}%>';
      $(document.body)
         .jqoteapp(mainTemplate, { a: 'PEET' });

   });

})(jQuery, window);
