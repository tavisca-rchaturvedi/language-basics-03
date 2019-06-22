using System;
using System.Linq;
using System.Collections;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        // Function to find Maximum Element Index
        // And also find index of other elements with same index
        public static ArrayList FindMax(int[] array, ArrayList Indices){
                
            int max = -1;
            ArrayList ind = new ArrayList();

            // If some indices have already been found in previous iteration
            // Checking whether there are some indices or not
            // Further filering indices of interest
            if(Indices != null && Indices.Count != 0){
                
                foreach(object o in Indices){
                    if(array[(int)o] > max){
                        max = array[(int)o];
                        ind = new ArrayList();
                        ind.Add((int)o);
                    }
                    else if(array[(int)o] == max){
                        ind.Add((int)o);
                    }
                }
                
            }
            // Finding the indices with maximum value
            else{
                
                for(int i = 0; i < array.Length; i++){
                    if(array[i] > max){
                        max = array[i];
                        ind = new ArrayList();
                        ind.Add(i);
                    }
                    else if(array[i] == max){
                        ind.Add(i);
                    }
                }
            }
            
            return ind;
        }
        
        // Similar function but for Minimum values
        // Same logic
        public static ArrayList FindMin(int[] array, ArrayList Indices){
            int min = 10000000;
            ArrayList ind = new ArrayList();
            if(Indices != null && Indices.Count > 0){
                //min = (int) Indices[0];
                foreach(object o in Indices){
                    if(array[(int)o] < min){
                        min = array[(int)o];
                        ind = new ArrayList();
                        ind.Add((int)o);
                    }
                    else if(array[(int)o] == min){
                        ind.Add((int)o);
                    }
                }
            }
            else{
                for(int i = 0; i < array.Length; i++){
                    if(array[i] < min){
                        min = array[i];
                        ind = new ArrayList();
                        ind.Add(i);
                    }
                    else if(array[i] == min){
                        ind.Add(i);
                    }
                }
            }
            
            return ind;
        }


        // Function to find minumum and maximum values according to required 
        public static ArrayList findMinDiet(string plan, int[] protein, int[] carbs, int[] fat, int[] calories, ArrayList Indices = null){
            ArrayList ind = new ArrayList();
            
            if(plan.Equals("f")){
                    ind = FindMin(fat, Indices);
            }
            else if(plan.Equals("F")){
                   ind = FindMax(fat, Indices);
            }
            else if(plan.Equals("t")){
                    ind = FindMin(calories, Indices);
            }
            else if(plan.Equals("T")){
                    ind = FindMax(calories, Indices);
            }
            else if(plan.Equals("c")){
                    ind = FindMin(carbs, Indices);
            }
            else if(plan.Equals("C")){
                    ind = FindMax(carbs, Indices);
            }
            else if(plan.Equals("p")){
                    ind = FindMin(protein, Indices);
            }
            else if(plan.Equals("P")){
                    ind = FindMax(protein, Indices);
            }
            return ind;
        }
        
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            //throw new NotImplementedException();
            
            // Calories Calculation
            int[] calories = new int[protein.Length];
            for(int i = 0; i < carbs.Length; i++){
                calories[i] = 9*fat[i] + 5*(carbs[i]+protein[i]);
            }
            
            //results Array
            int[] results = new int[dietPlans.Length];
            
            for(int i = 0; i < dietPlans.Length; i++){
                string plan = dietPlans[i];
                ArrayList indices = new ArrayList();
                // Diet Plan of length greater than 1
                
                if(plan.Length > 1){
                    for(int j = 0; j < plan.Length; j++){
                        // finding the index list of minimum values
                        ArrayList minInd = findMinDiet(plan[j].ToString(), protein, carbs, fat, calories, indices);
                        
                        // if only one maximum or minimum element is left 
                        // save it in result and break the inner loop
                        if(minInd.Count == 1){
                            results[i] = (int)minInd[0];
                            break;
                        }

                        // if more than one elements are left, wait for next iteration
                        else{
                            indices = minInd;
                        }
                        results[i] = (int)minInd[0];
                    }
                }
                
                // Diet Plan has unit length
                else if(plan.Length == 1){
                   results[i] = (int)findMinDiet(plan, protein, carbs, fat, calories)[0];
                }
            }
            
            return results;
            
        }
    }
}
