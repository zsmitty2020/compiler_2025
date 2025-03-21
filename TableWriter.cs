using System.Net.Mail;

namespace lab{

    public static class TableWriter{
        public static List< Tuple<string, int> > badList = new();
        public static string reduceConflict = null;
        public static int reduceConflictState = -1;
        public static void create(){
            //create a file called ParseTable.cs which has the parse table
            using( var w = new StreamWriter("ParseTable.cs") ){
                w.WriteLine("namespace lab{");
                w.WriteLine("public static class ParseTable{");
                
                w.WriteLine("    public static List<Dictionary<string,ParseAction> > table = new() {");

                for(int i=0;i<DFA.allStates.Count;++i){
                    w.WriteLine("        // DFA STATE "+i); //index in allStates == state's "unique" number
                    w.WriteLine("        new Dictionary<string,ParseAction>(){");
                    //shift rules
                    DFAState q = DFA.allStates[i];
                    foreach( string sym in q.transitions.Keys){
                        w.Write("                ");
                        w.Write("{");
                        w.Write($"\"{sym}\" , ");
                        w.Write($"new ParseAction(PAction.SHIFT, {q.transitions[sym].unique}, null, -1)");
                        w.WriteLine("},");
                    }
                    
                    //Shift-Reduce Conflict checking
                    
                    foreach( LRItem I in q.label.items){
                        if( I.dposAtEnd ){
                            foreach( string lookahead in I.lookaheads){
                                if( q.transitions.Keys.Contains( lookahead ) ){
                                    badList.Add( new Tuple<string, int>(lookahead, i ));
                                }
                            }
                        }
                    }

                    List< string > reducedList = new();
                    //reduce rules
                    foreach( LRItem I in q.label.items){
                        if( I.dposAtEnd ){
                            w.WriteLine($"            // {I}");
                            foreach( string lookahead in I.lookaheads){
                                if( badList.Contains( new Tuple<string, int>(lookahead, i))){

                                    continue;
                                }
                                if( reducedList.Contains( lookahead ) ){
                                    reduceConflict = lookahead;
                                    reduceConflictState = i;
                                    break;
                                }
                                w.Write($"            ");
                                w.Write("{");
                                w.Write($"\"{lookahead}\"");
                                w.Write(",");
                                w.Write($"new ParseAction(PAction.REDUCE, {I.production.rhs.Length}, \"{I.production.lhs}\", {I.production.unique})");
                                w.WriteLine("},");
                                reducedList.Add( lookahead );
                            }
                        }
                    }
                    if( i == DFA.allStates.Count - 1){
                        w.WriteLine("        }");
                    }
                    else
                        w.WriteLine("        },");
                }

                w.WriteLine("    }; //close the table initializer");

                //Shift-Reduce badList creation
                w.Write("\tpublic static List< Tuple<string, int> > badList = new List< Tuple<string, int> >(){");
                for(int i = 0; i < badList.Count(); i++){
                    if( badList[i].Item2 == badList[badList.Count()-1].Item2)
                        w.Write($"new Tuple<string, int>(\"{badList[i].Item1}\", {badList[i].Item2})");
                    else
                        w.Write($"new Tuple<string, int>(\"{badList[i].Item1}\", {badList[i].Item2}), ");
                }
                w.WriteLine("};");
                
                //Dump function
                w.WriteLine("    public static void dump(){");

                w.WriteLine("        int count = 0;");
                w.WriteLine("        foreach(var dict in table){");
                w.WriteLine("            Console.WriteLine($\"Row {count}:\");");

                //Shift-Reduce Conflict printing
                w.WriteLine("            for( int i = 0; i < badList.Count(); i++){");
                w.WriteLine("                if( count == badList[i].Item2){");
                w.WriteLine("                    Console.WriteLine($\"Shift-Reduce conflict in state {count} on symbol {badList[i].Item1}\");");
                w.WriteLine("                }");
                w.WriteLine("            }");
                
                //Reduce-Reduce Conflict printing
                if ( reduceConflict != null ){
                w.WriteLine($"                if( count == {reduceConflictState} ){{");
                w.WriteLine($"                    Console.WriteLine(\"Reduce-Reduce conflict in state {reduceConflictState} on symbol {reduceConflict}\");");
                w.WriteLine("                    Environment.Exit(99);");
                w.WriteLine("                }");
                }


                w.WriteLine("            foreach(var key in dict.Keys){");
                w.WriteLine("                Console.WriteLine($\"\\t{key} {dict[key].action} {dict[key].num} {dict[key].sym}\");");
                w.WriteLine("            }");
                w.WriteLine("            count++;");
                w.WriteLine("        }");

                w.WriteLine("    } //End dump");

                w.WriteLine("} //close the ParseTable class");
                w.WriteLine("} //close the namespace lab thing");




            }//End Create Function
            /*
                    new(){      //one of these for each DFA state
                        { "ID" , new(...) },    //one of these for each shift
                        { "ID" , new(...) },    //one of these for each reduction
                    }
            */

        }

    } //class TableWriter

} //namespace lab