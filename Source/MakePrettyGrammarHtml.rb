require 'yaml'

class Rules
  class << self
    include Enumerable
    
    def load
      data = YAML.load_file("Grammar.yaml")
      @rules = []
      data.keys.sort_by {|k| k.downcase}.each do |k|
        @rules << Rule.new(k, data[k]['Doc'], data[k])
      end
      @rules_hash = {}
      @rules.each do |r|
        @rules_hash[r.name] = r
      end
    end
    
    def [](name)
      @rules_hash[name]
    end
    
    def each
      @rules.each do |r|
        yield r
      end
    end
    
    def get_link_html(name, show_warnings)
      rule_name = name.to_s.split[0]
      if Rules[rule_name].nil?
        css_class = "unknownRule"
      else
        css_class = Rules[rule_name].css_class
      end
      if show_warnings && css_class != "normal" && css_class != "complete" && css_class != "started"
        puts "Reference to #{css_class} rule #{rule_name}"
      end
      %Q|<a href="##{rule_name}" class="#{css_class}">#{name}</a>|
    end
    
    def length
      @rules.length
    end
  end
end

class CommentLine
  def initialize(text)
    text = text.rstrip
    matchdata = %r{//\s*(.*)}.match(text)
    if !matchdata
      raise "Invalid comment line format: <#{$text}>"
    end
    @text = matchdata[1]
  end
  
  def count_as_complete?(default)
    default
  end
  
  def html
    %Q|<p>#{wikified_text}</p>\n|
  end
  
  def referenced_rule_names
    []
  end
  
  def wikified_text
    wikiword_regex = /\[\[([^\]]*)\]\]/
    @text.gsub(wikiword_regex) do |s|
      matchdata = wikiword_regex.match(s)
      Rules.get_link_html(matchdata[1], true)
    end
  end
  
  attr_reader :text
end

class Line
  def initialize(rule, text)
    @rule = rule
    text = text.rstrip
    matchdata = /^([!\.]) (->|  ) (.*)/.match(text)
    if !matchdata
      raise "Invalid line format: #{text.inspect}"
    end
    @done_status = matchdata[1]
    @arrow = matchdata[2] == '->'
    @text = matchdata[3]
  end
  
  def arrow_html
    if @arrow
      "<tt class=arrow>-&gt;</tt>"
    else
      "<tt class=arrow>&nbsp;&nbsp;</tt>"
    end
  end
  
  def completion_html
    if done?
      %Q|<span class="completed">(Completed)</span>|
    else
      %Q|<span class="inprogress">(In Progress)</span>|
    end
  end
  
  def count_as_complete?(default)
    done?
  end
  
  def css_class
    "normal"
  end
  
  def done?
    @done_status == "!"
  end
  
  def html
    %Q|<div class="#{css_class}">| +
      %Q|#{completion_html} #{arrow_html} #{text_html}</div>\n|
  end
  
  def referenced_rule_names
    text.scan(/[A-Z][a-z]\w*/)
  end
  
  def text_html
    text.
    gsub('&', '&amp;').
    gsub('<', '&lt;').
    gsub('>', '&gt;').
    gsub(/'(.)'/, "'<tt class=literal><b>\\1</b></tt>'").
    gsub(/"(.)"/, %Q|"<tt class=literal><b>\\1</b></tt>"|).
    gsub(/([A-Z][a-z]\w*)/) do |wikiword|
      Rules.get_link_html(wikiword, true)
    end
  end
  
  def to_s
    "#{arrow} #{text}"
  end
  
  attr_reader :arrow, :rule, :text
end

class Rule
  def initialize(name, data, yaml)
    @name = name
    @lines = data.rstrip.collect {|l| make_line(l)}
    @yaml = yaml
  end
  
  def complete?
    lines.all? {|line| line.count_as_complete?(true)}
  end
  
  def css_class
    if complete?
      "complete"
    elsif started?
      "started"
    else
      "normal"
    end
  end
  
  def html
    result = []
    result << %Q|<h2 class="#{css_class}"><a name="#{name}"></a>| +
      %Q|<a href="##{name}" class="black">#{name}</a> <a href="#toc" class="backtotop">[^]</a>|
    backlinks = referenced_by_rule_names
    if backlinks.any?
      backlinks_html = backlinks.collect {|name| Rules.get_link_html(name, false) }
      result << %Q|\n<div class="backlinks">Backlinks: | +
        %Q|#{backlinks_html.join(", ")}</div>|
    elsif name != "Goal"
      puts "No backlinks: #{name}"
    end
    result << "</h2>\n"
    lines.each do |l|
      result << l.html
    end
    result.join
  end
  
  def likely_target?
    !complete? && referenced_rules_started?
  end
  
  def make_line(l)
    if l =~ %r{//}
      CommentLine.new(l)
    else
      Line.new(self, l)
    end
  end
  
  def referenced_by_rule_names
    referenced_by_rules.collect {|r| r.name }
  end
  
  def referenced_by_rules
    @referenced_by_rules ||=
      Rules.select {|r| r.references_rule_name?(name) }
  end
  
  def referenced_rule_names
    @referenced_rule_names ||=
      lines.inject([]) {|acc, l| acc + l.referenced_rule_names}.uniq
  end
  
  def referenced_rules
    @referenced_rules ||= referenced_rule_names.collect do |name|
      r = Rules[name]
      if r.nil?
        raise "Invalid reference to rule name #{name}"
      end
      r
    end
  end
  
  def referenced_rules_started?
    referenced_rules.inject(true) {|acc, r| acc && r.started?}
  end
  
  def references_rule_name?(name)
    referenced_rule_names.include?(name)
  end
  
  def started?
    lines.any? {|line| line.count_as_complete?(false)}
  end
  
  def to_s
    %Q|= #{name} =\n#{lines.join("\n")}|
  end
  
  attr_reader :name, :lines, :yaml
end

class FileMaker
  def initialize(file)
    @file = file
  end
  
  def complete_rules
    Rules.select {|r| r.complete? }
  end
  
  def completion_message
    c = complete_rules.length
    t = Rules.length
    ratio = c * 100 / t
    "Completed #{c} rules of #{t} = #{ratio}% complete."
  end
  
  def execute
    write_html_header
    write_intro_section
    write_toc_section
    write_likely_targets_section
    write_rule_sections
  end
  
  def likely_target_names
    likely_targets.collect {|r| "#{r.name} (#{r.referenced_by_rules.length})"}.sort
  end
  
  def likely_targets
    Rules.select {|r| r.likely_target?}
  end
  
  def puts(s)
    @file.puts(s)
  end
  
  def rule_names
    Rules.collect {|r| r.name}
  end
  
  def rules_html
    Rules.collect {|r| r.html}.join
  end
  
  def toc_html(rules, columns)
    <<EOF
<div class="toc">
<table cellspacing="0" cellpadding="0" border="0" width="100%">
#{toc_rows_html(rules, columns)}
</table>
</div>
EOF
  end
  
  def toc_rows_html(rules, max_columns)
    row_count = (rules.length + (max_columns - 1)) / max_columns
    rows = []
    current_row = 0
    rules.each do |name|
      rows[current_row] = (rows[current_row] || "") +
        %Q|<td>#{Rules.get_link_html(name, false)}</td>|
      current_row = (current_row + 1) % row_count
    end
    "<tr>" + rows.join("</tr>\n<tr>") + "</tr>"
  end
  
  def write_html_footer
    puts "</body></html>"
  end
  
  def write_html_header
    puts "<html><head><title>Delphi Grammar</title></head>"
    puts "<link rel=\"stylesheet\" href=\"Grammar.css\"/>"
    puts "<body>"
  end
  
  def write_intro_section
    puts "<h1>DGrok Delphi Grammar</h1>"
    puts "<p>DGrok is my project to write a parser for the Delphi"
    puts "language, and then to build interesting tools on top of"
    puts "that parser. For more information on DGrok, see the"
    puts "<a href=\"http://excastle.com/blog/category/2.aspx?Show=All\">"
    puts "DGrok posts on my blog</a>.</p>"
    puts "<p>This page shows the Delphi grammar as I've"
    puts "<a href='http://excastle.com/blog/archive/2007/08/28/48084.aspx'>"
    puts "puzzled it out</a> so far, and indicates how much of it"
    puts "I've written a working parser for. Solid underline means"
    puts "that rule is completely implemented in my parser; broken"
    puts "underline means partly implemented; no underline means"
    puts "something I haven't started yet.</p>"
    puts "<p><b>Current status:</b> #{completion_message}</p>"
    puts "<p>Last updated #{Time.new}.</p>"
    puts "<p>&mdash; <a href='http://www.excastle.com/blog/'>Joe White</a></p>"
  end
  
  def write_likely_targets_section
    puts "<h2>Likely Targets (#{likely_targets.length})</h2>"
    puts "<p>These rules look like their dependencies are met,"
    puts "and are likely targets to be implemented next. Numbers"
    puts "in parentheses show how many places the rule is used.</p>"
    puts toc_html(likely_target_names, 6)
  end
  
  def write_rule_sections
    puts rules_html
  end
  
  def write_toc_section
    puts "<h2><a name='toc'>Table of Contents</a></h2>"
    puts toc_html(rule_names, 6)
  end
  
  attr_reader :data
end

Rules.load
File.open("Grammar.html", "w") do |f|
  FileMaker.new(f).execute
end