*Lợi ích của việc dùng automapper (theo như mình hiểu)

Bởi vì với AutoMapper bạn không phải thực hiện các phương pháp đó ;-)

cách tiếp cận của bạn đòi hỏi phải viết rất nhiều

[cách cũ]
classA.propA = classB.propA; 
classA.propB = classB.propB; 
classA.propC = classB.propC; 
classA.propD = classB.propD; 
classA.propE = classB.propE; 

[cách dùng mapper]
  var listPostCategoryViewModel = Mapper.Map<List<classA>,List<classB>>(listCategory);

vì vậy ta dùng auto mapper để có thể dùng lại nhiều lần


