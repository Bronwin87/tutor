﻿using System;
using System.Collections.Generic;

namespace Tutor.Web.Mappings.Knowledge.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsArchived { get; set; }
    public List<KnowledgeUnitDto> KnowledgeUnits { get; set; }
}