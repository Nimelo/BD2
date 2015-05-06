using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Converters
{
    public static class TemplateConverter
    {
        private static List<OUT> ConvertTemplate<OUT>(List<Tuple<int, object>> collection)
            where OUT : class, new()
        {
            List<OUT> returnList = new List<OUT>();
            if(collection.Count == 0)
                return returnList;
            var inputProperties = collection.First().Item2.GetType().GetProperties();
            var outputProperties = typeof(OUT).GetProperties();



            foreach(var item in collection)
            {
                inputProperties = item.Item2.GetType().GetProperties();
                var el = new OUT();
                el.GetType().GetField("Id").SetValue(el, item.Item1);
                foreach(var prop in inputProperties)
                {
                    if(( outputProperties.ToList().Exists(x => x.Name == prop.Name
                        && x.PropertyType == prop.PropertyType) ))
                    {
                        var elProp = el.GetType().GetProperty(prop.Name);
                        elProp.SetValue(el, prop.GetValue(item.Item2));
                    }
                }

                returnList.Add(el);
            }


            return returnList;
        }

        public static List<OUT> Convert<OUT>(List<object> collection)
            where OUT : class, new()
        {
            List<OUT> returnList = new List<OUT>();
            if(collection.Count == 0)
                return returnList;


            var type = collection.First().GetType();
            List<Tuple<int, object>> templateCollection = new List<Tuple<int, object>>();
            if(type == typeof(CandidatesServiceReference.Candidate))
            {
                collection.ForEach(x => templateCollection.Add(
                    new Tuple<int, object>(
                        ( (UI.CandidatesServiceReference.Candidate)x ).Id,
                        ( (UI.CandidatesServiceReference.Candidate)x ).Person)
                        ));
            }
            else if(type == typeof(PersonsServiceReference.Person))
            {          
                collection.ForEach(x => templateCollection.Add(
                    new Tuple<int, object>(
                        ( (PersonsServiceReference.Person)x ).Id,
                        x)
                        ));

            }
            else if(type == typeof(CandidatesServiceReference.Document))
            {
                
                collection.ForEach(x =>
                    
                    {     string Type = ( (CandidatesServiceReference.Document)x ).Type.ToString();
                    templateCollection.Add(
                new Tuple<int, object>(
                    (
                    (CandidatesServiceReference.Document)x ).Id,
                    new
                    {
                        ( (CandidatesServiceReference.Document)x ).Name,
                        ( (CandidatesServiceReference.Document)x ).Extension,
                        Type
                    }
                    )
                    );
                    });
                     
            }
            else if(type == typeof(CandidatesServiceReference.SkillsEvaluation))
            {
                collection.ForEach(x => templateCollection.Add(
                   new Tuple<int, object>(
                       ( (CandidatesServiceReference.SkillsEvaluation)x ).Id,
                       new
                       {
                           ((CandidatesServiceReference.SkillsEvaluation)x ).Mark,
                           ((CandidatesServiceReference.SkillsEvaluation)x ).Skill.Name
                       })
                       ));
            }
            else if(type == typeof(CandidatesServiceReference.SoftSkillsEvaluation))
            {
                collection.ForEach(x => templateCollection.Add(
                   new Tuple<int, object>(
                       ( (CandidatesServiceReference.SoftSkillsEvaluation)x ).Id,
                       new
                       { 
                          ((CandidatesServiceReference.SoftSkillsEvaluation)x).Mark,
                          ((CandidatesServiceReference.SoftSkillsEvaluation)x ).SoftSkill.Name
                       })
                       ));
            }
            else if(type == typeof(DictionaryServiceReference.Skill))
            {
                collection.ForEach(x => templateCollection.Add(
                  new Tuple<int, object>(
                      ( (DictionaryServiceReference.Skill)x ).Id,
                        x
                      )));
            }
            else if(type == typeof(DictionaryServiceReference.SoftSkill))
            {
               collection.ForEach(x => templateCollection.Add(
                  new Tuple<int, object>(
                      ( (DictionaryServiceReference.SoftSkill)x ).Id,
                        x
                      )));
            }
            else if(type == typeof(DictionaryServiceReference.Stage))
            {
                collection.ForEach(x => templateCollection.Add(
                  new Tuple<int, object>(
                      ( (DictionaryServiceReference.Stage)x ).Id,
                        x
                      )));
            }
            else if(type == typeof(CandidatesServiceReference.RecruitmentStage))
            {
                collection.ForEach(x => templateCollection.Add(
                   new Tuple<int, object>(
                       ( (CandidatesServiceReference.RecruitmentStage)x ).Id,
                       new
                       {
                           ( (CandidatesServiceReference.RecruitmentStage)x ).Mark,
                           ( (CandidatesServiceReference.RecruitmentStage)x ).Stage.Name
                       })
                       ));
            }
            else
            {
                return returnList;
            }

            return ConvertTemplate<OUT>(templateCollection);  

        }
    }
}
